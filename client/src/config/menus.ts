import { AppMenuItem } from "@app/common";
let menus: Array<AppMenuItem> = [
    new AppMenuItem("Order Management", "", "fa fa-home", [
        new AppMenuItem("Orders", "/orderManagement/orders")
    ]),
    new AppMenuItem("Security", "", "fa fa-home", [
        new AppMenuItem("Permissions", "/security/permissions")
    ]),
    new AppMenuItem("Setting", "", "fa fa-edit", [
        new AppMenuItem("ContentTypes", "/setting/contentTypes")
    ])
];
export default menus;