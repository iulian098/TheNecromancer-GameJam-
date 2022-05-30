using System;
using UnityEngine;

namespace DialogSystem {

    public class DialogSystem : MonoBehaviour {

        public static DialogSystem Instance;
        private void Awake() {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        [SerializeField] DialogContainer container;
        [SerializeField] DialogBox dialogBox;
        public Action OnDialogEnd;

        public void ShowDialog(string name) {
            Dialog diag = container.GetDialogByName(name);
            dialogBox.ShowBox(diag);
        }
    }
}
