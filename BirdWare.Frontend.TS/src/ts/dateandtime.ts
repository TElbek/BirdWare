export const formatDate = (date: string) => {
    const options: Intl.DateTimeFormatOptions = {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    };

    return date ? new Date(date).toLocaleDateString('da-DK', options) : '';    
};

export const formatDateDDMM = (date: string) => {
    const options: Intl.DateTimeFormatOptions = {
        day: '2-digit',
        month: 'short'
    };

    return date ? new Date(date).toLocaleDateString('da-DK', options) : '';    
};

export const getNameOfMonth = (month: number) => {
    let date = new Date();
    date = new Date(date.getFullYear(), month - 1, 1);
    const options: Intl.DateTimeFormatOptions = {month: 'long'};
    let nameOfMonth = date.toLocaleDateString('da-DK',options);
    return nameOfMonth.slice(0,1).toUpperCase() + nameOfMonth.slice(1);
}

export const getMonthNameFromNumber = (value: number) => {
    const options: Intl.DateTimeFormatOptions = {
        month: 'long',
    };
    return new Date('2025-' + value + '-01').toLocaleDateString('da-DK', options);
}

export const getCurrentSeasonName = () => {
    const month = new Date().getMonth() + 1;
    let saesonNavn: string = "";

    switch(month) {
        case 1:
        case 2:
        case 12: 
            saesonNavn = 'Vinter';
            break;
        case 3:
        case 4:
        case 5: 
            saesonNavn = 'Forår';
            break;
        case 6:
        case 7:
        case 8: 
            saesonNavn = 'Sommer';
            break;
        case 9:
        case 10:
        case 11: 
            saesonNavn = 'Efterår';
            break;
    }

    return saesonNavn;
}