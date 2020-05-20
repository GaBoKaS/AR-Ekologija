using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public GameObject TimeText;
    public int SecondsLeft;
    private string minutes;
    private string seconds;


    public GameObject ScoreText;
    public int Score;

    public GameObject WaveText;
    public int Wave;

    public GameObject Title;
    public string titleString = "Rezultatas";

    void Update()
    {
        if(SecondsLeft / 60 < 10)
            minutes = "0" + (SecondsLeft / 60 );
        else minutes = "" + (SecondsLeft / 60);

        if (SecondsLeft % 60 < 10)
            seconds = "0" + (SecondsLeft % 60);
        else seconds = "" + (SecondsLeft % 60);

        TimeText.gameObject.transform.GetComponent<TMPro.TextMeshPro>().text = minutes + ":" + seconds;
        ScoreText.gameObject.transform.GetComponent<TMPro.TextMeshPro>().text = Score + "";
        WaveText.gameObject.transform.GetComponent<TMPro.TextMeshPro>().text = Wave + "";
        Title.gameObject.transform.GetComponent<TMPro.TextMeshPro>().text = titleString;
    }
    // Screen color changing function
    public GameObject Screen;
    public Material DefaultMaterial;
    public Material GreenMaterial;
    public Material RedMaterial;

    public void ChangeColor(string color)
    {
        Material material;
        if (color.Equals("Green"))
            material = GreenMaterial;
        else if (color.Equals("Red"))
            material = RedMaterial;
        else material = DefaultMaterial;

        Screen.GetComponent<Renderer>().material = material;
    }
}
