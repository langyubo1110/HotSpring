function formatDateString(obj, key) {
    return obj[key].replace("/Date(", "").replace(")/", "");
}