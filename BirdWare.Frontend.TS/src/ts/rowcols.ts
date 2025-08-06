export const getRowColClasses = (itemCount: number) => {
    if(itemCount == 1) {
        return 'row-cols-1';
    }
    else if (itemCount == 2) {
        return 'row-cols-1 row-cols-md-2';
    }
    else if (itemCount == 3) {
        return 'row-cols-1 row-cols-md-2 row-cols-lg-3';
    }
    else if (itemCount >= 4) {
        return 'row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4';
    }
}

export const getRowColClassesThree = (itemCount: number) => {
    if(itemCount == 1) {
        return 'row-cols-1';
    }
    else if (itemCount == 2) {
        return 'row-cols-1 row-cols-md-2';
    }
    else {
        return 'row-cols-1 row-cols-md-2 row-cols-xl-3';
    }
}