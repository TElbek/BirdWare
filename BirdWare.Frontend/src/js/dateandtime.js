export const formatDate = (date) => {
    const options = {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    };

    return new Date(date).toLocaleDateString('da-DK', options);
};

export const getNameOfMonth = (month) => {
    let date = new Date();
    date = new Date(date.getFullYear(), month - 1, 1);
    const options = {month: 'long'};
    let nameOfMonth = date.toLocaleDateString(date, options);
    return nameOfMonth.slice(0,1).toUpperCase() + nameOfMonth.slice(1);
}

export const getMonthNameFromNumber = (value) => {
    const options = {
        month: 'long',
    };
    return new Date('2025-' + value + '-01').toLocaleDateString('da-DK', options);
}