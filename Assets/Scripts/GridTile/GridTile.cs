using UnityEngine;

    public class GridTile : Tile
    {
        [SerializeField] GameObject applePrefab;
        private GameObject apple;
        bool hasApple = false;
        public bool HasApple { get { return hasApple; } }
        public Color[] colors;
        public bool SetApple()
        {
            if (hasApple)
                return false;
            else
            {
                apple = Instantiate(applePrefab, transform.position, Quaternion.identity);
                apple.transform.parent = transform;
                apple.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                hasApple = true;
                return true;
            }
        }

        public bool TakeApple()
        {
            if (!hasApple)
                return false;
            else
            {
                hasApple = false;
                Destroy(apple.gameObject);
                apple = null;
                return true;
            }
        }
    }
