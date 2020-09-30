PUBLIC readMSR   
PUBLIC writeMSR
PUBLIC readPMIO1  
PUBLIC writePMIO1
PUBLIC readPMIO2  
PUBLIC writePMIO2
PUBLIC readPMIO4  
PUBLIC writePMIO4

PUBLIC hypercallHook
PUBLIC hypercallOriginalLocation
PUBLIC hypercallPreCallHandler


_DATA SEGMENT
	hypercallOriginalLocation db 0,0,0,0,0,0,0,0
	hypercallPreCallHandler db 0,0,0,0,0,0,0,0	
_DATA ENDS

.CODE _text

	readMSR PROC PUBLIC
		push rbx
		mov rbx, rdx
		RDMSR 
		mov [rbx], edx
		mov [rbx+4], eax
		pop rbx
		ret
	readMSR ENDP 

	writeMSR PROC PUBLIC
		mov eax, r8d
		WRMSR
		ret
	writeMSR ENDP 

	readPMIO1 PROC PUBLIC
		mov edx, ecx
		in al, dx
		ret
	readPMIO1 ENDP 

	writePMIO1 PROC PUBLIC
		mov eax, edx
		mov edx, ecx
		out dx, al
		ret
	writePMIO1 ENDP 

	readPMIO2 PROC PUBLIC
		mov edx, ecx
		in ax, dx
		ret
	readPMIO2 ENDP 

	writePMIO2 PROC PUBLIC
		mov eax, edx
		mov edx, ecx
		out dx, ax
		ret
	writePMIO2 ENDP 

	readPMIO4 PROC PUBLIC
		mov edx, ecx
		in eax, dx
		ret
	readPMIO4 ENDP 

	writePMIO4 PROC PUBLIC
		mov eax, edx
		mov edx, ecx
		out dx, eax
		ret
	writePMIO4 ENDP 

	hypercallHook PROC PUBLIC
		push rcx
		push rdx
		push r8
		lea rax, hypercallPreCallHandler
		mov rax, [rax]
		test rax, rax
		jz skipHypercallLog
			sub rsp, 20h
			call rax
			add rsp, 20h

		skipHypercallLog:
		pop r8
		pop rdx
		pop rcx
		lea rax, hypercallOriginalLocation
		mov rax, [rax]
		jmp rax
	hypercallHook ENDP 
END                                                                                                                                                                                                                      