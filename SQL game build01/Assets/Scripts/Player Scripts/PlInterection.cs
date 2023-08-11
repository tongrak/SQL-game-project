using Gameplay.ChaptersNRooms;
using Puzzle.PuzzleController;
using System.Collections;
using UnityEngine;


namespace Gameplay.Player
{
    public delegate void InteractionHandler(IPuzzleController pm);

    public class PlInterection : MonoBehaviour
    {
        //Dynamic object
        private IPuzzleController _interectedPM;
        private RoomChangingScript _interestedTraverseZone;
        //Event raiser
        public event InteractionHandler InteractionCalled;
        public event RoomTraverseHandler RoomTraverseCalled;
        [Header("Configure Variable")]
        [SerializeField] private int _traverseWaitingTime = 1;
        [SerializeField] private float _interactionBufferTime = 0.5f;
        //Dynamic Var
        [SerializeField] private bool _canInteract = true;
        public bool CanInteract
        {
            get => _canInteract;
            set => _canInteract = value;
        }
        private bool _interactionCall = false;
        private bool _iPointDetected = false;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "I-Point":
                    _interectedPM = collision.gameObject.GetComponent<IPuzzleController>();
                    if (_interectedPM == null) Debug.LogWarning("IPoint detected but cann't receive PuzzleMaster");
                    else _iPointDetected = true;
                    break;
                case "Room Changing Zone":
                    _interestedTraverseZone = collision.gameObject.GetComponent<RoomChangingScript>();
                    Debug.Log("Enter changing zone");
                    if (_interestedTraverseZone == null) Debug.LogWarning("Traverse Zone detected but cann't receive Direction");
                    else StartCoroutine(RoomsTraverseBuffer());
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "I-Point":
                    _interectedPM = null;
                    break;
                case "Room Changing Zone":
                    StopCoroutine(RoomsTraverseBuffer());
                    _interestedTraverseZone = null;
                    break;
            }
        }

        #region Input Handlering
        private void InputUpdate()
        {
            _interactionCall = Input.GetButtonDown("Interact");
        }

        private void InputEnforcer()
        {
            if (_interactionCall && _canInteract)
            {
                if (_iPointDetected) PassPM();

                _canInteract = false;
                _interactionCall = false;
                StartCoroutine(InteractionBuffer());
            }
        }
        #endregion

        #region Room terverse handlering
        IEnumerator RoomsTraverseBuffer()
        {
            yield return new WaitForSeconds(_traverseWaitingTime);

            if (_interestedTraverseZone != null)
            {
                Debug.Log("Pl-Interaction: travelling...");
                RoomTraverse();
            }
            else throw new System.Exception("Cann't traval to interested room; Due to : Fail to get travalling zone script");
        }
        #endregion

        #region Misc Function
        public virtual void RoomTraverse()
        {
            RoomTraverseCalled?.Invoke(_interestedTraverseZone.travelDirection);
        }
        public virtual void PassPM()
        {
            InteractionCalled?.Invoke(_interectedPM);
        }
        private IEnumerator InteractionBuffer()
        {
            yield return new WaitForSeconds(_interactionBufferTime);
            _canInteract = true;
        }
        #endregion

        // Update is called once per frame
        void Update()
        {
            InputUpdate();

            InputEnforcer();
        }
    }
}
