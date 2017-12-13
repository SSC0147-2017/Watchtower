using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager SM;

    AudioSource Source;

    public AudioClip[] Achievement;
    public AudioClip[] Clone;
    public AudioClip[] Explosion;
    public AudioClip[] Attack;
    public AudioClip[] HobbesGrunt;
	public AudioClip[] CorJackGrunt;
	public AudioClip[] ArwinGrunt;
    public AudioClip[] Hit;
    public AudioClip[] Bugfolk;
    public AudioClip[] BugfolkGrunt;
    public AudioClip[] Depths;
    public AudioClip[] DepthsGrunt;
    public AudioClip[] Pistol;
    public AudioClip[] Crossbow;
    public AudioClip[] Button;
    public AudioClip[] CharSwoosh;
	public AudioClip[] ItemPickup;
	public AudioClip[] Flag;

    private void Start()
    {
        if(SoundManager.SM == null)
        {
            SoundManager.SM = this;
        }
        if(this != SoundManager.SM)
        {
            Destroy(this);
        }

        Source = GetComponent<AudioSource>();
    }

    public void PlayAchievement()
    {
        int index = Random.Range(0, Achievement.Length-1);
        Source.PlayOneShot(Achievement[index], 0.5f);
    }

    public void PlayClone()
    {
        int index = Random.Range(0, Clone.Length-1);
        Source.PlayOneShot(Clone[index], 0.5f);
    }

    public void PlayExplosion()
    {
        int index = Random.Range(0, Explosion.Length-1);
        Source.PlayOneShot(Explosion[index], 0.5f);
    }

    public void PlayAttack()
    {
        int index = Random.Range(0, Attack.Length-1);
        Source.PlayOneShot(Attack[index], 0.5f);
    }

    public void PlayHobbesGrunt()
    {
        int index = Random.Range(0, HobbesGrunt.Length-1);
        Source.PlayOneShot(HobbesGrunt[index], 0.5f);
    }
	
	public void PlayCorJackGrunt()
    {
        int index = Random.Range(0, CorJackGrunt.Length-1);
        Source.PlayOneShot(CorJackGrunt[index], 0.5f);
    }
	
	public void PlayArwinGrunt()
    {
        int index = Random.Range(0, ArwinGrunt.Length-1);
        Source.PlayOneShot(ArwinGrunt[index], 0.5f);
    }

    public void PlayHit()
    {
        int index = Random.Range(0, Hit.Length-1);
        Source.PlayOneShot(Hit[index], 0.5f);
    }

    public AudioClip GetBugfolk()
    {
        int index = Random.Range(0, Bugfolk.Length-1);
        return Bugfolk[index];
    }

    public AudioClip GetBugfolkGrunt()
    {
        int index = Random.Range(0, BugfolkGrunt.Length-1);
        return BugfolkGrunt[index];
    }

    public AudioClip GetDepths()
    {
        int index = Random.Range(0, Depths.Length-1);
        return Depths[index];
    }

    public AudioClip GetDepthsGrunt()
    {
        int index = Random.Range(0, DepthsGrunt.Length-1);
        return DepthsGrunt[index];
    }

    public void PlayPistol()
    {
        int index = Random.Range(0, Pistol.Length-1);
        Source.PlayOneShot(Pistol[index], 0.5f);
    }

    public void PlayCrossbow()
    {
        int index = Random.Range(0, Crossbow.Length-1);
        Source.PlayOneShot(Crossbow[index], 0.5f);
    }

    public void PlayButton()
    {
        int index = Random.Range(0, Button.Length-1);
        Source.PlayOneShot(Button[index], 0.5f);
    }

    public void PlayCharSwoosh()
    {
        int index = Random.Range(0, CharSwoosh.Length-1);
        Source.PlayOneShot(CharSwoosh[index], 0.5f);
    }

	public void PlayItemPickup()
	{
		int index = Random.Range(0, ItemPickup.Length-1);
		Source.PlayOneShot(ItemPickup[index], 0.5f);
	}

	public void PlayFlag()
	{
		int index = Random.Range(0, Flag.Length-1);
		Source.PlayOneShot(Flag[index], 0.5f);
	}

}
