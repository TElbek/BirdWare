export function useDebounce() {
    function debounce<F extends (...args: any[]) => any>(func: F, waitFor: number): (...args: Parameters<F>) => void {
        let timeoutId: number | undefined;

        return function (...args: Parameters<F>): void {
            if (timeoutId !== undefined) {
                clearTimeout(timeoutId);
            }
            timeoutId = setTimeout(() => func(...args), waitFor);
        };
    }
    return { debounce };
}