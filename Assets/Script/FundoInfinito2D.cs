using UnityEngine;

public class FundoInfinito2D : MonoBehaviour
{
    // Referência da câmera (geralmente a Main Camera)
    public Transform cameraTransform;

    // Largura e altura do sprite
    private float largura;
    private float altura;

    // Posição inicial do fundo
    private Vector3 startPos;

    void Start()
    {
        // Guarda a posição inicial
        startPos = transform.position;

        // Pega o SpriteRenderer do objeto
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // Calcula largura e altura do sprite
        largura = sprite.bounds.size.x;
        altura = sprite.bounds.size.y;
    }

    void Update()
    {
        // Calcula a distância da câmera para o fundo (horizontal)
        float distX = cameraTransform.position.x - transform.position.x;

        // Calcula a distância da câmera para o fundo (vertical)
        float distY = cameraTransform.position.y - transform.position.y;

        // Se a câmera passou da largura do fundo
        if (Mathf.Abs(distX) >= largura)
        {
            // Decide se move para direita ou esquerda
            float moverX = distX > 0 ? largura : -largura;

            // Move o fundo para frente criando efeito infinito
            transform.position += new Vector3(moverX * 3, 0, 0);
        }

        // Se a câmera passou da altura do fundo
        if (Mathf.Abs(distY) >= altura)
        {
            // Decide se move para cima ou para baixo
            float moverY = distY > 0 ? altura : -altura;

            // Move o fundo para cima ou baixo criando efeito infinito
            transform.position += new Vector3(0, moverY * 3, 0);
        }
    }
}

