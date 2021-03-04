using UnityEngine.UI;
using UnityEngine;

public class Track : MonoBehaviour
{
    public PlayerInfoKeep keeper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //player ID
    public void playerIDGet()
    {
        keeper.PlayerID = this.GetComponent<InputField>().text;
    }
}
