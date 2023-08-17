import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { VimeoVideo } from './vimeo-video.model';

@Component({
  selector: 'app-vimeo-upload',
  templateUrl: './vimeo-upload.component.html',
  styleUrls: ['./vimeo-upload.component.scss']
})
export class VimeoUploadComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  uploading = false;
  lastUploadError = false;
  uploadedVideos: Array<VimeoVideo> = [];

  statusMap = {
    complete: 'complete',
    error: 'error',
    in_progress: 'in_progress'
  }

  statusMapCard: {[id: string] : string} = {
    [this.statusMap.complete]: 'border-success',
    [this.statusMap.error]: 'border-error',
    [this.statusMap.in_progress]: 'border-secondary'
  };

  constructor(private http: HttpClient) {}

  upload(event: any): void {
    const file: File= event.target.files[0];
    if (file && !this.uploading) {
      const formData = new FormData();

      formData.append('file_data', file);

      this.uploading = true;
      this.lastUploadError = false;

      this.http.post<VimeoVideo>('/api/vimeo', formData)
        .subscribe({
          next: (response: VimeoVideo) => {
            this.uploadedVideos.push(response);
            this.uploading = false;

          },
          error: () => {
           this.lastUploadError = true;
            this.uploading = false;
          }
        });

    }
    this.fileInput.nativeElement.value = '';
  }

  checkStatus(uri: string): void {
    const video = this.uploadedVideos.find(f => f.uri === uri);

    this.http.get<VimeoVideo>(`/api/vimeo/${uri.split('/')[2]}/status`)
      .subscribe({
        next: (response: VimeoVideo) => {
          video!.transcode = response.transcode;
          video!.upload = response.upload;
        }
      });
  }
}
