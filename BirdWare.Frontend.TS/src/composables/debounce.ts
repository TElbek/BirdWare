/**
 * Composable for creating debounced functions.
 * Debouncing ensures that a function is only executed after a certain amount of time has passed since the last call.
 * This is useful for limiting the rate of execution for functions that are called frequently, such as in search inputs or resize handlers.
 */
export function useDebounce() {
    /**
     * Creates a debounced version of the provided function.
     * @param func - The function to debounce.
     * @param waitFor - The number of milliseconds to wait before executing the function.
     * @returns A debounced function that delays execution until after the wait time has elapsed since the last call.
     */
    function debounce<F extends (...args: any[]) => any>(func: F, waitFor: number): (...args: Parameters<F>) => void {
        let timeoutId: number | undefined; // Stores the ID of the current timeout to allow cancellation

        return function (...args: Parameters<F>): void {
            if (timeoutId !== undefined) {
                clearTimeout(timeoutId); // Cancel the previous timeout to reset the delay
            }
            timeoutId = setTimeout(() => func(...args), waitFor); // Schedule the function execution after the specified wait time
        };
    }
    return { debounce };
}