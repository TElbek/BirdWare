export const getRowColClassesFive = (itemCount: number) => {
    const classList = ['sm:grid-cols-1', 'md:grid-cols-2', 'lg:grid-cols-3', 'xl:grid-cols-4', '2xl:grid-cols-5'];
    return 'grid gap-4 ' + classList.slice(0, Math.min(5, itemCount)).join(' ');
}

export const getRowColClassesFour = (itemCount: number) => {
    const classList = ['sm:grid-cols-1', 'md:grid-cols-2', 'lg:grid-cols-3', 'xl:grid-cols-4'];
    return 'grid gap-4 ' + classList.slice(0, Math.min(4, itemCount)).join(' ');
}

export const getRowColClassesThree = (itemCount: number) => {
    const classesThree = ['sm:grid-cols-1', 'md:grid-cols-2', 'xl:grid-cols-3'];
    return 'grid gap-4 ' + classesThree.slice(0, Math.min(3, itemCount)).join(' ');
}

export const getRowColClassesTwo = (itemCount: number) => {
    const classesThree = ['sm:grid-cols-1', 'md:grid-cols-2'];
    return 'grid gap-4 ' + classesThree.slice(0, Math.min(2, itemCount)).join(' ');
}