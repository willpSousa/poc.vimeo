import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { VimeoUploadComponent } from './vimeo-upload/vimeo-upload.component';
import { CommonModule } from '@angular/common';
import { SafePipe } from './safe-pipe.pipe';
import { VimeoVideoLocalstorageService } from './vimeo-upload/vimeo-video-localstorage.service';

@NgModule({
  declarations: [
    AppComponent,
    VimeoUploadComponent,
    SafePipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: VimeoUploadComponent, pathMatch: 'full' }
    ])
  ],
  providers: [VimeoVideoLocalstorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
