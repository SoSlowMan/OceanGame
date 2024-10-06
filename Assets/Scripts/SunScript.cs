// Changes the color of the light source depending on time (time = full day cycle in sec) with Lerp function. Rotation of the sun is implemented, but timing is thinnicky.

using UnityEngine;

public class SunScript : MonoBehaviour
{
    [SerializeField] Light sun;
    [SerializeField] float speedSun, time;
    [SerializeField] Color[] colors;
    [SerializeField] int currentColorIndex, nextColorIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentColorIndex = 0;
        nextColorIndex = 1;
        speedSun = 1f;
        sun = GetComponent<Light>(); //init
    }

    // Update is called once per frame
    void Update()
    {
        Transition();
    }

    void Transition()
    {
        speedSun += Time.deltaTime / time;
        sun.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], speedSun);
        transform.Rotate(Vector3.right, Time.deltaTime / 2);

        if (speedSun >= 1f)
        {
            Debug.Log("Drop");
            speedSun = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex++;

            if (nextColorIndex == colors.Length)
            {
                nextColorIndex = 0;
            }
        }
    }
}
