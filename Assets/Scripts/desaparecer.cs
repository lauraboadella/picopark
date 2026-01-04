using UnityEngine;

public class desaparecer : MonoBehaviour
{
    public GameObject[] plataformas; 
    public float tiempoCambio = 2f;

    float timer;
    bool imparesActivas = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tiempoCambio)
        {
            timer = 0f;
            imparesActivas = !imparesActivas;
            ActualizarPlataformas();
        }
    }

    void ActualizarPlataformas()
    {
        
        plataformas[0].SetActive(imparesActivas);
        plataformas[2].SetActive(imparesActivas);
        plataformas[4].SetActive(imparesActivas);

        
        plataformas[1].SetActive(!imparesActivas);
        plataformas[3].SetActive(!imparesActivas);
    }
}
