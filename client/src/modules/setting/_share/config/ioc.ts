import { IoCNames, IoCLifeCycle } from "@app/common";
import { SettingService } from "@app/setting";
let ioc = [
    { name: IoCNames.ISettingService, instance: SettingService, lifeCycle: IoCLifeCycle.Singleton }
];
export default ioc;