export const LoadingIndicatorEvent = {
    Show: "show",
    Hide: "hide"
};


export const AuthenticatedEvent = {
    AuthenticationChanged: "AuthenticationChanged"
};

export const ApplicationStateEvent = {
    Init: "ApplicationStateEvent.Init",
    BeforeReady: "ApplicationStateEvent.BeforeReady",
    Ready: "ApplicationStateEvent.Ready",
    Unload: "ApplicationStateEvent.Unload",
    UnAuthorizeRequest: "UnAuthorizeRequest"
};

export const LANG = {
    EN: "en"
};

export enum InputValueType {
    Text,
    Currency
};
export enum HttpStatusCode {
    OK = 200
};
export const Guid = {
    Empty: "00000000-0000-0000-0000-000000000000"
};

export const HttpCode = {
    NotFound: 404,
    UnAuthorized: 401,
    BadRequest: 400
};

export enum FormMode {
    AddNew,
    Edit
};

export const Locale = {
    Setting: "setting",
    Common: "common",
    Security: "security"
};

export const ModuleNames = {
    Security: "security",
    Setting: "setting",
    Common: "common",
    CustomerManagement:"customerManagement",
    Dashboard:"dashboard",
    Order:"orderManagement"
};

export enum PageActionItemType{
    Button,
    Dropdown
}