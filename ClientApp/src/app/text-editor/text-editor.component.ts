import { Component, OnInit, ViewChild } from '@angular/core';
import {EditorChangeContent, EditorChangeSelection} from 'ngx-quill';

@Component({
  selector: 'app-text-editor',
  styleUrls: ['./text-editor.component.scss'],
  templateUrl: './text-editor.component.html',
})
export class TextEditorComponent implements OnInit {
  editorText ='';
  constructor() {
  }

  ngOnInit() {

  }
  onChangeEditor(event: EditorChangeContent | EditorChangeSelection){
      console.log(event);
      this.editorText = event['editor']['root']['innerHTML'];
  }

}
