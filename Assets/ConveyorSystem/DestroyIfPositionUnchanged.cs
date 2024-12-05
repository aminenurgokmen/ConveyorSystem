using UnityEngine;

public class DestroyIfPositionUnchanged : MonoBehaviour
{
    private Vector3Int previousPosition; // Önceki pozisyonun tam sayı hali
    private float unchangedTime = 0f; // Değişmeden geçen süre
    public float destroyThreshold = 3f; // Değişmezlik süresi eşiği (saniye)

    private void Start()
    {
        // Başlangıçta önceki pozisyonu ayarla
        previousPosition = Vector3Int.RoundToInt(transform.position);
    }

    private void Update()
    {
        // Şu anki pozisyonun tam sayı halini al
        Vector3Int currentPosition = Vector3Int.RoundToInt(transform.position);

        if (currentPosition == previousPosition)
        {
            // Eğer pozisyon değişmediyse süreyi artır
            unchangedTime += Time.deltaTime;

            // Süre eşik değerini geçerse objeyi yok et
            if (unchangedTime >= destroyThreshold)
            {
                Debug.Log($"Object {gameObject.name} has not moved for {destroyThreshold} seconds and will be destroyed.");
                Destroy(gameObject);
            }
        }
        else
        {
            // Eğer pozisyon değiştiyse süreyi sıfırla ve önceki pozisyonu güncelle
            unchangedTime = 0f;
            previousPosition = currentPosition;
        }
    }
}
