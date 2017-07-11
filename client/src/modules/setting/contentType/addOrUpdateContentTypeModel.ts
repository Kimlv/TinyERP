import { BaseModel, ValidationException } from "@app/common";
export class AddOrUpdateContentTypeModel extends BaseModel {
    public id: string = String.empty;
    public name: string = String.empty;
    public key: string = String.empty;
    public description: string = String.empty;
    public parameters: Array<any> = [];

    public import(item: any) {
        this.id = item.id;
        this.name = item.name;
        this.key = item.key;
        this.description = item.description;
        if (item.parameters) {
            this.parameters = item.parameters;
        }
    }

    protected getValidationErrors(): ValidationException {
        let exception: ValidationException = new ValidationException();
        if (String.isNullOrWhiteSpace(this.name)) {
            exception.add("setting.addOrUpdateContentType.validation.nameIsRequired");
        }
        return exception;
    }
}