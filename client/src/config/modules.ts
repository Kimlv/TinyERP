import { ModuleNames, IModuleConfigItem } from "@app/common";
let modules: Array<IModuleConfigItem> = [
    { name: ModuleNames.Security, urlPrefix: ModuleNames.Security, path: ModuleNames.Security, isDefault: true },
    { name: ModuleNames.Setting, urlPrefix: ModuleNames.Setting, path: ModuleNames.Setting},
    { name: ModuleNames.Order, urlPrefix: ModuleNames.Order, path: ModuleNames.Order}
];
export default modules;