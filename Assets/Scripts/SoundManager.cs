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
    public AudioClip[] PlayerGrunt;
    public AudioClip[] Hit;
    public AudioClip[] Monster;
    public AudioClip[] MonsterGrunt;
    public AudioClip[] Pistol;
    public AudioClip[] Crossbow;
    public AudioClip[] Button;
    public AudioClip[] CharSwoosh;

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
        int index = Random.Range(0, Achievement.Length);
        Source.PlayOneShot(Achievement[index]);
    }

    public void PlayClone()
    {
        int index = Random.Range(0, Clone.Length);
        Source.PlayOneShot(Clone[index]);
    }

    public void PlayExplosion()
    {
        int index = Random.Range(0, Explosion.Length);
        Source.PlayOneShot(Explosion[index]);
    }

    public void PlayAttack()
    {
        int index = Random.Range(0, Attack.Length);
        Source.PlayOneShot(Attack[index]);
    }

    public void PlayPlayerGrunt()
    {
        int index = Random.Range(0, PlayerGrunt.Length);
        Source.PlayOneShot(PlayerGrunt[index]);
    }

    public void PlayHit()
    {
        int index = Random.Range(0, Hit.Length);
        Source.PlayOneShot(Hit[index]);
    }

    public void PlayMonster()
    {
        int index = Random.Range(0, Monster.Length);
        Source.PlayOneShot(Monster[index]);
    }

    public void PlayMonsterGrunt()
    {
        int index = Random.Range(0, MonsterGrunt.Length);
        Source.PlayOneShot(MonsterGrunt[index]);
    }

    public void PlayPistol()
    {
        int index = Random.Range(0, Pistol.Length);
        Source.PlayOneShot(Pistol[index]);
    }

    public void PlayCrossbow()
    {
        int index = Random.Range(0, Crossbow.Length);
        Source.PlayOneShot(Crossbow[index]);
    }

    public void PlayButton()
    {
        int index = Random.Range(0, Button.Length);
        Source.PlayOneShot(Button[index]);
    }

    public void PlayCharSwoosh()
    {
        int index = Random.Range(0, CharSwoosh.Length);
        Source.PlayOneShot(CharSwoosh[index]);
    }
}
