import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllInquiriesComponent } from './all-inquiries/all-inquiries.component';
import { MyInquiriesComponent } from './my-inquiries/my-inquiries.component';
import { MyPitchesComponent } from './my-pitches/my-pitches.component';

const routes: Routes = [
    { path: '', component: AllInquiriesComponent },
    { path: 'my-inquiries', component: MyInquiriesComponent },
    { path: 'my-pitches', component: MyPitchesComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class InquiresRoutingModule { }
