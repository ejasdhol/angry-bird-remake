using UnityEngine;
using UnityEngine.SceneManagement; 

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround; 

    [SerializeField] private float _launchPower = 250;
    

    private void awake()
    {
        _initialPosition = transform.position; 

    }

    private void update()
    {
        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude<= 0.1)
        {
            _timeSittingAround += Time.deltaTime; 
        }

        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 ||
            _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name; 
            SceneManager.LoadScene(currentSceneName);
        }
            
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        
        Vector2 directionToInitialPosition = _initialPosition - transform.position; 

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true; 
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(newPosition.x, newPosition.y); 
    }
}
