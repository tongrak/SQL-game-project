using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleController
{
    public class QueryPuzzleToGetKeyItemController : QueryPuzzleController, IPuzzleController
    {
        [SerializeField] KeyItem keyItem;

        public new PuzzleType PuzzleType { get; protected set; } = PuzzleType.QueryPuzzleToGetKeyItem;

        public new KeyItem GetKeyItem()
        {
            return keyItem;
        }

        void Awake()
        {
            // keep value from puzzle file to this object
            LoadPuzzle();
            // 3) validate answer query
            SQLValidator validator = SQLValidator.GetInstance();
            validator.validatePathAndQuery(DBPath, AnswerQuery);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
