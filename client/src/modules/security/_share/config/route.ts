let routes: any = getRoute();
export default routes;
function getRoute() {
    let route: any = {
        permission: {
            permissions: {
                name: "security.permission.permissions",
                path: "permissions"
            },
            addPermission: {
                name: "security.permission.addPermission",
                path: "permissions/addPermission"
            },
            editPermission: {
                name: "security.permission.editPermission",
                path: "permissions/:id/edit"
            }
        }
    };
    return route;
}