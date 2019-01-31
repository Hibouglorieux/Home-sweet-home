using UnityEngine;

public class SucceedSound : MonoBehaviour
{
    //public static SucceedSound inst;

    static AudioSource _source;

    void Awake()
    {
        //inst = this;
        _source = GetComponent<AudioSource>();
    }

    public static void Succeed()
    {
        _source.time = 0;
        _source.Play();
    }
}
