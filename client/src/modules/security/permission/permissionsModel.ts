import { IoCNames } from "@app/common";
export class PermissionsModel {
    public options: any = {};
    public eventKey: string = "permisisons_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        this.options = {
            columns: [
                { field: "name", title: resourceHelper.resolve("security.permissions.grid.name") },
                { field: "key", title: resourceHelper.resolve("security.permissions.grid.key") },
                { field: "description", title: resourceHelper.resolve("security.permissions.grid.description") }
            ],
            data: [],
            enableEdit: true,
            enableDelete: true
        };
    }
    public addPageAction(action: any) {
        this.actions.push(action);
    }
    public import(items: Array<any>) {
        let eventManager = window.ioc.resolve(IoCNames.IEventManager);
        eventManager.publish(this.eventKey, items);
    }
}