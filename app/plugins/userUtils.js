
export default ({ store }, inject) => {

  let roles = ["guest", "user", "pro", "admin"] // Sort hierarchically the different roles.

  inject('userLoggedIn', () => {
    if (store.state.auth) return store.state.auth.user.role == "user"
    return false;
  })

  inject('userIsAtLeast', (role) => {
    // Ensure the user is hierarcly equals or higher than the given role and that the given roles exists.
    if (store.state.auth) return roles.indexOf(store.state.auth.user.role) >= roles.indexOf(role) && roles.indexOf(role) != -1;
    return false;
  })

  inject('userRoleIs', (role) => {
    if (store.state.auth) return store.state.auth.user.role == role;
    return false;
  })

  inject('userIdIs', (userId) => {
    if (store.state.auth) return store.state.auth.user.id == userId;
    return false
  })

}