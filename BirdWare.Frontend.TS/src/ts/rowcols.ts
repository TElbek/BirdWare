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
    else if (itemCount == 4) {
        return 'row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4';
    }
    else if (itemCount == 5) {
        return 'row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-5';
    }
    else if (itemCount >= 6) {
        return 'row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 row-cols-xxl-5 row-cols-xxl-6';
    }
}

export const getRowColClassesTwo = (itemCount: number) => {
        if(itemCount == 1) {
        return 'row-cols-1';
    }
    else if (itemCount >= 2) {
        return 'row-cols-1 row-cols-md-2';
    }
}