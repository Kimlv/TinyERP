import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BaseControl } from "../../models/ui";
@Component({
    selector: "form-text-input",
    templateUrl: "src/modules/common/components/form/formTextInput.html"
})
export class FormTextInput extends BaseControl {
    @Input() labelText: string = String.empty;
    @Input() placeHolderText: string = String.empty;
    @Input() validation: Array<string> = [];
    @Input() model: any;
    @Input() readOnly: boolean = false;
    @Output() modelChange = new EventEmitter();
    public onValueChanged(evt: any) {
        this.modelChange.emit(evt.target.value);
    }
}