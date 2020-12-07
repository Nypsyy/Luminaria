using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource source;
    public PlayerController controller;
    public Tilemap[] tilemaps;

    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (controller.isGrounded && Mathf.Abs(rb2D.velocity.x) > 0.01f)
        {
            if (!source.isPlaying && source.clip != null)
            {
                source.Play();
            }
        }
        else
            source.Stop();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Tilemap t = collision.collider.gameObject.GetComponent<Tilemap>();

        if (t == tilemaps[0])
        {
            source.clip = AudioManager.instance.sounds[0].clip;
            source.volume = AudioManager.instance.sounds[0].volume;
        }
        else
        {
            source.clip = AudioManager.instance.sounds[3].clip;
            source.volume = AudioManager.instance.sounds[3].volume;
        }
    }
}
