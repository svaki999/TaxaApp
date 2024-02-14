function addFareToFirestore(fareData) {
    return firebase.firestore().collection('fares').add(fareData);
}