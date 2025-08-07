export const getRowColClasses = (itemCount: number) => {
    const classesFour = ['row-cols-1', 'row-cols-md-2', 'row-cols-lg-3', 'row-cols-xl-4'];
    return classesFour.slice(0, Math.min(4, itemCount)).join(' ');
}

export const getRowColClassesThree = (itemCount: number) => {
    const classesThree = ['row-cols-1', 'row-cols-md-2', 'row-cols-xl-3'];
    return classesThree.slice(0, Math.min(3, itemCount)).join(' ');
}

export const getRowColClassesTwo = (itemCount: number) => {
    const classesThree = ['row-cols-1', 'row-cols-md-2'];
    return classesThree.slice(0, Math.min(2, itemCount)).join(' ');
}