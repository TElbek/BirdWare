export const valueIsNumber = (value: string | number): value is number => {
    return typeof value === 'number' && !isNaN(value);
}