using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyScript : MonoBehaviour
{
    // [SerializeField] [Range(1, 2)] int _cactusType;
    [SerializeField] bool _cactusType;

    
    // Не думай что придираюсь, но буду оставлять коменты что я думаю
    // Range лучше использовать где есть ну вот ограничение
    // + если использовать так то лучше short.
    // а вообще наверно логичнее и удобнее bool

    [SerializeField] Transform shootingStartPoint;
    [SerializeField] GameObject enemyWeapon;
    [SerializeField] float shootRate;

    private float _shootTime;
    private bool _isPlayerHere;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Первый тип - постоянно стреляющий через shootRate секунд
        if(_cactusType)
        {
            RangedShooting();
            /*
            Прочто на заметку:
            Если всего 2 типа то можно(может лучше хз) использовать bool
            и лучше начинать имена булов с is, например isPlayerDetectingType

            О ты и так так пишеш оказца )) потом увидел ) Красава

            Если например 5-6 типов то можно использовать Enum
            Если надо обьяснить пиши в личку, но прежде погугли ))
            */
        }

        // Второй тип - появляется и стреляет тогда, когда игрок заходит в определенную зону
        if(!_cactusType && _isPlayerHere)
        {
            RangedShooting();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _isPlayerHere = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _isPlayerHere = false;
        }
    }

    private void RangedShooting()
    {
        if(Time.time > _shootTime)
        {
            Instantiate(enemyWeapon, shootingStartPoint.position, transform.rotation);
            _shootTime = Time.time + shootRate;
        }
    }
    
}
