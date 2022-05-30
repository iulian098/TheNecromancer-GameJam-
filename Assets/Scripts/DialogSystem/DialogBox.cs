using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DialogSystem {
    public class DialogBox : MonoBehaviour {
        [SerializeField] Animator anim;
        [SerializeField] TMP_Text characterName;
        [SerializeField] TMP_Text text;
        Tween doTweenAnim;
        Dialog dialog;
        int dialogIndex = 0;

        private void OnEnable() {
            anim.Play("Show");
        }

        private void OnDisable() {
            
        }

        public void ShowBox(Dialog dialog) {
            this.dialog = dialog;
            dialogIndex = 0;

            gameObject.SetActive(true);
            characterName.text = dialog.Dialogs[dialogIndex].Name;
            text.text = dialog.Dialogs[dialogIndex].Text;
            dialog.Dialogs[dialogIndex].onShow?.Invoke();
        }

        public void ShowNext() {
            dialogIndex++;

            if (dialogIndex == dialog.Dialogs.Length - 1) {
                anim.Play("Hide");
                DialogSystem.Instance.OnDialogEnd?.Invoke();
                return;

            }


            anim.SetTrigger("Next");

            characterName.text = dialog.Dialogs[dialogIndex].Name;
            text.text = dialog.Dialogs[dialogIndex].Text;
            dialog.Dialogs[dialogIndex].onShow?.Invoke();
        }

        public void HideBox() {
            gameObject.SetActive(false);
        }
    }
}
