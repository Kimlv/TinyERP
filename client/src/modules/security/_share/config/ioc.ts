import { IoCNames, IoCLifeCycle } from "@app/common";
import { PermissionService } from "@app/security";
let ioc = [
    { name: IoCNames.IPermissionService, instance: PermissionService, lifeCycle: IoCLifeCycle.Singleton }
];
export default ioc;