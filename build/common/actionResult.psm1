using module ".\enum.psm1"
class ActionResult{
	[ActionStatusType] $Status=[ActionStatusType]::None
	[string]$Message = ""
	ActionResult([ActionStatusType] $status){
		$this.Status = $status
	}
}