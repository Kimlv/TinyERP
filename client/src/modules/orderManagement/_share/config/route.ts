let routes: any = getRoute();
export default routes;
function getRoute() {
    let route: any = {
        order: {
            orders: {
                name: "orderManagement.order.orders",
                path: "orders"
            }
        }
    };
    return route;
}