using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementBehaviour : MonoBehaviour {

    Vector3 InitialPosition;
    Vector3 TargetPosition;
    public float EntryTime;
    public float ExitTime;
    public float StayDelay;

    private void OnEnable()
    {
        this.Start();
    }
    // Use this for initialization
    void Start () {
        SoundManager.SM.PlayAchievement();

        InitialPosition = transform.position;
		TargetPosition = new Vector3(Screen.width/2, Screen.height/6, 0);

        iTween.MoveTo(
            gameObject,
            iTween.Hash(
                "x", TargetPosition.x,
                "y", TargetPosition.y,
                "easetype", "easeInOutQuad",
                "time", EntryTime));

        StartCoroutine(Delay(StayDelay));
    }
	
	IEnumerator Delay(float StayDelay)
    {
        yield return new WaitForSeconds(StayDelay);

        iTween.MoveTo(
            gameObject,
            iTween.Hash(
                "x", InitialPosition.x,
                "y", InitialPosition.y,
                "easetype", "easeInOutQuad",
                "time", ExitTime,
                "oncomplete", "Closed"));
    }

    void Closed()
    {
        transform.position = InitialPosition;
        gameObject.SetActive(false);
    }
}
