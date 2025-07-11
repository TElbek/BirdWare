export const formatDate = (date: string) => {
    const options: Intl.DateTimeFormatOptions = {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    };

    return new Date(date).toLocaleDateString('da-DK', options);
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