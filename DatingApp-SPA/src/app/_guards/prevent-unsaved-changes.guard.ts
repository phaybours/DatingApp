import { Injectable } from '@angular/core';
// tslint:disable-next-line: max-line-length
// import { CanActivate, CanActivateChild, CanDeactivate, CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { CanActivate, Router, CanDeactivate } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
// import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
// export class AuthGuard implements CanActivate, CanActivateChild, CanDeactivate<unknown>, CanLoad {
export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent> {
    canDeactivate(component: MemberEditComponent): boolean {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
        }
        return true;
    }
}
