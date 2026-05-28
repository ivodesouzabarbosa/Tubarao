using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PeixeControl : MonoBehaviour
{
    public int _levelPeixe;
    public Color[] _corHit;
    MouseControl tubarao;
    [SerializeField] protected GameControl _gameControl;

    void Start()
    {
    _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
    _gameControl._peixe1List.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boca"))
        {
            //Debug.Log("Boca")

            GameObject boca = collision.gameObject;
            Transform transformTuba = boca.transform.parent.parent;
            MouseControl tubarao = transformTuba.GetComponent<MouseControl>();
            if (tubarao._levelTuba>= _levelPeixe)
            {
                transform.SetParent(tubarao._boca.transform);
                transform.DOScale(Vector3.zero, 1f);
                transform.DOLocalMove(Vector3.zero, 1f);
                Debug.Log("Som de ser comido");
               
            }
            else
            {
                tubarao.HitTuba();
                Debug.Log("Peixe hit e e recua");
                Debug.Log("Som de ser hit");
            }

           

        }
    }

   
        

}
