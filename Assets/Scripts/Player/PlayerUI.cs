using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/** 
* Updating the player ui textboxes. 
* @author: Oliver Thompson
* @since: 2025-05-25
*/
public class PlayerUI : MonoBehaviour
{
   
   [SerializeField] private TextMeshProUGUI promptText; // Reference to the player ui textMesh

   /** 
   * Update the text for the player ui.
   * @author: Oliver Thompson
   * @since: 2025-05-25
   * @param promptMesssage: The new text message.     
   */
   public void updateText(string promptMessage)
   {
      promptText.text = promptMessage;
   }

}
