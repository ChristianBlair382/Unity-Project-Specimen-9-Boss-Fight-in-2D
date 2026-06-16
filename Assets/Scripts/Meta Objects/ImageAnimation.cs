using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {

	public Sprite[] sprites;
	public int spritePerFrame = 6;
	public float speed = 1f;
	public bool loop = true;
	public bool destroyOnEnd = false;

	private int index = 0;
	private Image image;
	private float frame = 0;

	void Awake() {
		image = GetComponent<Image> ();
	}

	void Update () {
		if (!loop && index == sprites.Length) return;
		frame += speed;
		if (frame < spritePerFrame) return;
		image.sprite = sprites [index];
		frame -= spritePerFrame;
		index ++;
		if (index >= sprites.Length) {
			if (loop) index = 0;
			if (destroyOnEnd) Destroy (gameObject);
		}
	}
}
