import { Promise } from "@app/common";
export interface IPermissionService {
    getPermissions(): Promise;
    getPermission(itemId: string): Promise;
    createPermission(model: any): Promise;
    updatePermission(model: any): Promise;
    deletePermission(itemId: string): Promise;
}