export const formatDate = (date) => {
    const options = {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    };

    return new Date(date).toLocaleDateString('da-DK', options);
};

export const getNameOfMonth = (month) => {
    var date = new Date();
    date = new Date(date.getFullYear(), month -1, date.getDay());
    const options = {month: 'long'};
    var nameOfMonth = date.toLocaleString(date, options);
    return nameOfMonth.slice(0,1).toUpperCase() + nameOfMonth.slice(1);
}