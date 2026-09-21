export async function getPendingPostCount(): Promise<number> {
  if (!indexedDB.databases) return 0

  const databases = await indexedDB.databases()

  if (!databases.some(db => db.name === 'workbox-background-sync')) {
    return 0
  }

  return new Promise((resolve, reject) => {
    const request = indexedDB.open('workbox-background-sync')

    request.onerror = () => reject(request.error)

    request.onsuccess = () => {
      const db = request.result
      const storeName = 'api-post-queue'

      if (!db.objectStoreNames.contains(storeName)) {
        db.close()
        resolve(0)
        return
      }

      const transaction = db.transaction(storeName, 'readonly')
      const countRequest = transaction.objectStore(storeName).count()

      countRequest.onsuccess = () => {
        db.close()
        resolve(countRequest.result)
      }

      countRequest.onerror = () => {
        db.close()
        reject(countRequest.error)
      }
    }
  })
}