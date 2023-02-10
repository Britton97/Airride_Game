using UnityEngine;
using UnityEngine.EventSystems;
using System;

using Photon.Pun;

using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;

namespace Com.MyCompany.MyGame
{
    [RequireComponent(typeof(PhotonView), typeof(NewPlayerMovement))]
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(IsFiring);
                stream.SendNext(IsFrozen);
            }
            else
            {
                // Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.IsFrozen = (bool)stream.ReceiveNext();
            }
        }
        #endregion

        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        public GameObject PlayerUiPrefab;

        public WhichTeam playerTeam;

        [Header("Player Roles")]
        public List<WhichTeam> teamList = new List<WhichTeam>();

        #endregion

        #region Private Fields

        [Tooltip("The Beams GameObject to control")]
        [SerializeField]
        private GameObject beams;
        //True, when the user is firing
        bool IsFiring;
        [HideInInspector]
        public bool IsFrozen;
        [HideInInspector]
        [Tooltip("The current Health of our player")]
        public float Health = 1f;
        #endregion

        #region Honestly I dont know what this is for

        #if UNITY54ORNEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
        #endif


        #endregion
        
        #region MonoBehaviour CallBacks

        void Awake()
        {
            //PV = GetComponent<PhotonView>();
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

            if (beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
        }

        void Start()
        {
            CameraFollow _cameraFollow = this.gameObject.GetComponent<CameraFollow>();
            
            if (_cameraFollow != null)
            {
                if (photonView.IsMine)
                {
                    _cameraFollow.OnStartFollowing();
                }
            }
            
            if (PlayerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver); //call the set target function on every script that has a SetTarget function
            }

            GetComponent<NewPlayerMovement>().SetTarget(this);

            #if UNITY54ORNEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            #endif
        }

        #endregion

        #region Collider Methods
        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            //other player touched you
            //they then affect your team
            //their team scriptable object will decide for you what team you are on now
            if (!photonView.IsMine)
            {
                return;
            }
            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            PlayerManager otherPlayer = other.gameObject.GetComponentInParent<PlayerManager>();
            int newTeam = otherPlayer.playerTeam.TouchedPlayer(this.playerTeam.activeTeam);
            if(newTeam == -1 || newTeam == playerTeam.GetTeamNumber())
            {
                return;
            }
            else
            {
                SetTeam(newTeam);
            }
        }

        #endregion

        #region Level Loading Methods

        #if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        #endif

        #if UNITY54ORNEWER
        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable ();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endif

        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }

            GameObject _uiGo = Instantiate(this.PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        #endregion

        #region Inputs
        /// <summary>
        /// Processes the inputs. Maintain a flag representing when the user is pressing Fire.
        /// </summary>
        void ProcessInputs()
        {
            if(photonView.IsMine)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (!IsFiring)
                    {
                        IsFiring = true;
                    }
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    if (IsFiring)
                    {
                        IsFiring = false;
                    }
                }
            }
        }

        #endregion

        #region Test Game Manager Methods

        public void SetTeam(int _team)
        {
            if(photonView.IsMine)
            {
                photonView.RPC("RPC_SetTeam", RpcTarget.AllBuffered, _team);
                //SpawnTeamObjects(photonView.ViewID); //do not want to buffer spawn objects. it will spawn duplicates
            }
        }
        [PunRPC]
        private void RPC_SetTeam(int _team)
        {
            playerTeam = teamList[_team];
            
            playerTeam.EnterState(this);
            IsFrozen = playerTeam.cantMove;
        }

        //game manager should call this function when spawning a new player
        //then this function will call the spawn role objects function on the team scriptable object
        //the scriptable object should take in the id of the photon view of the player
        //spawn each individual object and set the parent to the photon view id in an rpc function to sync over the server


        //this should not take a parameter
        //it should send the photon view id to the scriptable object
        //then the SO should do all of the heavy lifting like setting the parent
        //and running the RPC
        public void SpawnTeamObjects()
        {
            if(photonView.IsMine)
            {            
                int id = photonView.ViewID; //grab the photon view id of the player
                GameObject passed = PhotonView.Find(id).gameObject;
                playerTeam.SpawnRoleObjects(id);
                //g.SendMessage("SetParent", id, SendMessageOptions.RequireReceiver); //call the set target function on every script that has a SetTarget function

                playerTeam.SpawnRoleUIs(GameObject.Find("Ability Panel").gameObject);
            }     
        }

        #endregion
    }
}