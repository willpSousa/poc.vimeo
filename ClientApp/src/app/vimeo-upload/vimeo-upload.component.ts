import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { VimeoUploadResult } from './vimeo-upload-result.model';

@Component({
  selector: 'app-vimeo-upload',
  templateUrl: './vimeo-upload.component.html',
  styleUrls: ['./vimeo-upload.component.scss']
})
export class VimeoUploadComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  state: 'empty' | 'success' | 'error' = 'empty';
  uploading = false;

  uploadedVideos: Array<VimeoUploadResult> = [];

  constructor(private http: HttpClient) { }

  upload(event: any) {
    const file: File = event.target.files[0];
    if (file && !this.uploading) {
      const formData = new FormData();

      formData.append('file_data', file);

      this.uploading = true;

      this.uploadedVideos.push({
        fileName: file.name,
        videoUri: ''
      });

      this.http.post<VimeoUploadResult>('https://localhost:7013/api/vimeo', formData)
        .subscribe({
          next: (response: VimeoUploadResult) => {
            const videoResult = this.uploadedVideos.find(v => !v.videoUri);

            videoResult!.videoUri = response.videoUri;

            this.uploading = false;
          },
          error: () => {
            const videoResult = this.uploadedVideos.find(v => !v.videoUri);

            videoResult!.videoUri = 'Error';
            videoResult!.hasError = true;

            this.uploading = false;
          }
        });

    }
    this.fileInput.nativeElement.value = '';
  }
}
