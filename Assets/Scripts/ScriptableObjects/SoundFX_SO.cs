using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundFX_SO : ScriptableObject
{
   [SerializeField]
   public List<AudioClip> chop;
   
   [SerializeField]
   public List<AudioClip> deliveryFail;
   
   [SerializeField]
   public List<AudioClip> deliverySuccess;
   
   [SerializeField]
   public List<AudioClip> footstep;
   
   [SerializeField]
   public List<AudioClip> objectDrop;
   
   [SerializeField]
   public List<AudioClip> objectPickup;   
   
   [SerializeField]
   public List<AudioClip> sizzle;
   
   [SerializeField]
   public List<AudioClip> trash;
   
   [SerializeField]
   public List<AudioClip> warning;   
   
   
}
